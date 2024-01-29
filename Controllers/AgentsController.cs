using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Study_Abroad.Data;
using Study_Abroad.Models;

namespace Study_Abroad.Controllers
{
    public class AgentsController : Controller
    {
        private readonly StudyAbroadContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public AgentsController(StudyAbroadContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<RegisterModel> logger, IEmailSender emailSender)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        [BindProperty]
        public string ReturnUrl { get; set; }

        private List<AuthenticationScheme> ExternalLogins;

        // GET: Agents
        public async Task<IActionResult> Index()
        {
            var studyAbroadContext = _context.Agents.Include(a => a.Cities).Include(a => a.IdentityUser);
            return View(await studyAbroadContext.ToListAsync());
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .Include(a => a.Cities)
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName");
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userId != null)
            //{
            //    var user = await _userManager.FindByIdAsync(userId);
            //    var role = await _userManager.GetRolesAsync(user);
            //    if (role.ElementAt(0) == "Agent")
            //    {
            //        ReturnUrl = returnUrl;
            //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //        string link = Request.Scheme + "://" + Request.Host + "/StaffDetails/Create";
            //        return View();
            //    }
            //    else
            //    {
            //        string link = Request.Scheme + "://" + Request.Host + "/Identity/Account/Login";

            //        return Redirect(link);
            //    }

            //}

            //else
            //{
            //    string link = Request.Scheme + "://" + Request.Host + "/Identity/Account/Login";
            //    return Redirect(link);

            //}
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentId,IdentityUserId,AgentName,ContactNumber,Agent_Email,Agent_Passward,Address,CityId,Designation,Status,Date")] Agent agent, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //agent.Designation = "Agent";

            var user = new IdentityUser { UserName = agent.Agent_Email, Email = agent.Agent_Email, PhoneNumber = agent.ContactNumber, EmailConfirmed = true };
            agent.IdentityUser = user;
            var result = await _userManager.CreateAsync(user, agent.Agent_Passward);
            if (result.Succeeded)
            {
                await _roleManager.CreateAsync(new IdentityRole(agent.Designation));
                await _userManager.AddToRoleAsync(user, agent.Designation);

                _logger.LogInformation("User created a new account with password.");
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            //if (ModelState.IsValid)
            //{
            //    _context.Add(agent);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", agent.CityId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", agent.IdentityUserId);
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", agent.CityId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", agent.IdentityUserId);
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentId,IdentityUserId,AgentName,ContactNumber,Agent_Email,Agent_Passward,Address,CityId,Designation,Status,Date")] Agent agent)
        {
            if (id != agent.AgentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.AgentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "CityName", agent.CityId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", agent.IdentityUserId);
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agents
                .Include(a => a.Cities)
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.AgentId == id);
        }

        private async Task AgentCookies(IdentityUser user, string role, Agent agent)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("Email", user.Email));
            identity.AddClaim(new Claim("UserId", user.Id));
            identity.AddClaim(new Claim("AgentId", agent.AgentId.ToString()));
            identity.AddClaim(new Claim("Status", agent.Status.ToString()));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(2)
                });

        }
    }
}
