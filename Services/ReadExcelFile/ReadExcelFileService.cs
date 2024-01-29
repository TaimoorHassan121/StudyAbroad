using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Study_Abroad.DTO;
using Study_Abroad.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Study_Abroad.Services.ReadExcelFile
{
    public class ReadExcelFileService : IReadExcelFileInterface
    {
        public async Task<List<string>> ReadDisiplineExcelFile(IFormFile file)
        {
            var countries = new List<string>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;
                    string disiplineName = "";
                    int disiplineIndex = 0;
                    string value = "";
                    bool flag = true;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (flag)
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().Trim().ToLower();
                                if (string.IsNullOrEmpty(value)) continue;
                                if (GetDisiplineIndex(value))
                                {
                                    disiplineIndex = j;
                                    flag = false;
                                    break;
                                }
                            }
                            continue;
                        }
                        disiplineName = workSheet.Cells[i, disiplineIndex].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(disiplineName) || disiplineName.ToLower() == "disipline-name") continue;
                        //if (GetCountryIndex(workSheet.Cells[i, countryIndex].Value?.ToString().Trim().ToLower())) continue;
                        countries.Add(disiplineName);
                    }
                }
            }
            return countries;
        }
        public async Task<List<string>> ReadCountryExcelFile(IFormFile file)
        {
            var countries = new List<string>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;
                    string countryName = "";
                    int countryIndex = 0;
                    string value = "";
                    bool flag = true;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (flag)
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().Trim().ToLower();
                                if (string.IsNullOrEmpty(value)) continue;
                                if (GetCountryIndex(value))
                                {
                                    countryIndex = j;
                                    flag = false;
                                    break;
                                }
                            } 
                            continue;
                        }
                        countryName = workSheet.Cells[i, countryIndex].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(countryName) || countryName.ToLower() == "country-name") continue;
                        //if (GetCountryIndex(workSheet.Cells[i, countryIndex].Value?.ToString().Trim().ToLower())) continue;
                        countries.Add(countryName);
                    }
                }
            }
            return countries;
        }
        public async Task<List<ExcelStateDto>> ReadStateExcelFile(IFormFile file)
        {
            var states = new List<ExcelStateDto>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;
                    int countryIndex = 0;
                    int stateIndex = 0;
                    //bool flag = true;
                    bool countryflag = true;
                    bool stateflag = true;
                    string stateName = "";
                    string value = "";
                    string valueType = "";
                    int countryId = 0;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (countryflag && stateflag)
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().ToLower().Trim();
                                if (string.IsNullOrEmpty(value)) continue;
                                if (GetCountryIndex(value,true))
                                {
                                    countryIndex = j;
                                    countryflag = false;
                                }
                                else if (GetStateIndex(value))
                                {
                                    stateIndex = j;
                                    stateflag = false;
                                }
                                if (!countryflag && !stateflag)
                                    break;
                            }
                            continue;
                        }
                        stateName = workSheet.Cells[i, stateIndex].Value?.ToString().Trim();
                        value = workSheet.Cells[i, countryIndex].Value?.ToString().Trim();
                        valueType = workSheet.Cells[i, countryIndex].Value?.GetType().ToString();
                        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(stateName)) continue;
                        if(valueType.Contains("String") || stateName.ToLower() == "state-name") continue;
                        countryId = Convert.ToInt32(value);
                        if (countryId<=0) continue;
                        states.Add(new ExcelStateDto { name = stateName, id =  countryId });
                    }
                }
            }
            return states;
        }
        public async Task<List<ExcelUniDto>> ReadUniExcelFile(IFormFile file)
        {
            var universities = new List<ExcelUniDto>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;
                    bool uniflag = true;
                    bool stateflag = true;
                    bool countryflag = true;

                    string value = "";
                    string stateValue = "";
                    string stateValueType = "";
                    string countryValue = "";
                    string countryValueType = "";

                    int countryIndex = 0;
                    int stateIndex = 0;
                    int uniIndex = 0;

                    string uniName = "";
                    int stateId = 0;
                    int countryId = 0;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (uniflag && stateflag && countryflag)
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().ToLower().Trim();
                                if (string.IsNullOrEmpty(value)) continue;
                                if (GetUniIndex(value))
                                {
                                    uniIndex = j;
                                    uniflag = false;
                                }
                                else if (GetStateIndex(value, true))
                                {
                                    stateIndex = j;
                                    stateflag = false;
                                }
                                else if(GetCountryIndex(value, true))
                                {
                                    countryIndex = j;
                                    countryflag = false;
                                }

                                if (!uniflag && !stateflag && !countryflag)
                                    break;
                            }
                            continue;
                        }
                        if (countryIndex <= 0 || stateIndex <= 0 || uniIndex <= 0) continue;

                        uniName = workSheet.Cells[i, uniIndex].Value?.ToString().Trim();

                        stateValue = workSheet.Cells[i, stateIndex].Value?.ToString().Trim();
                        stateValueType = workSheet.Cells[i, stateIndex].Value?.GetType().ToString();

                        countryValue = workSheet.Cells[i, countryIndex].Value?.ToString().Trim();
                        countryValueType = workSheet.Cells[i, countryIndex].Value?.GetType().ToString();

                        if (string.IsNullOrEmpty(uniName) || string.IsNullOrEmpty(stateValue) || string.IsNullOrEmpty(countryValue)) continue;
                        if (stateValueType.Contains("String") || countryValueType.Contains("String") || uniName.ToLower() == "university-name" || uniName.ToLower() == "university-id") continue;
                       
                        stateId = Convert.ToInt32(stateValue);
                        countryId = Convert.ToInt32(countryValue);

                        if (stateId <= 0 || countryId<=0) continue;

                        universities.Add(new ExcelUniDto { uniName = uniName, countryId = countryId , stateId = stateId });
                    }
                }
            }
            return universities;
        }
        public async Task<List<ExcelUniDto>> ReadUniCampusExcelFile(IFormFile file)
        {
            var campuses = new List<ExcelUniDto>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;
                    bool uniflag = true;
                    bool campusflag = true;

                    string value = "";
                    string valueType = "";

                    int campusIndex = 2;
                    int uniIndex = 1;

                    string uniCampName = "";
                    int uniId = 0;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (uniflag && campusflag)
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().ToLower().Trim();
                                if (string.IsNullOrEmpty(value)) continue;
                                if (GetUniIndex(value,true))
                                {
                                    uniIndex = j;
                                    uniflag = false;
                                }
                                else if (GetCampusIndex(value))
                                {
                                    campusIndex = j;
                                    campusflag = false;
                                }

                                if (!uniflag && !campusflag)
                                    break;
                            }
                            continue;
                        }
                        uniCampName = workSheet.Cells[i, campusIndex].Value?.ToString().Trim();

                        value = workSheet.Cells[i, uniIndex].Value?.ToString().Trim();
                        valueType = workSheet.Cells[i, uniIndex].Value?.GetType().ToString();

                        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(uniCampName)) continue;
                        if (valueType.Contains("String") || string.IsNullOrEmpty(uniCampName) || uniCampName.ToLower() == "campus-name" || uniCampName.ToLower() == "campus-id") continue;
                        uniId = Convert.ToInt32(value);
                        if (uniId <= 0) continue;
                        campuses.Add(new ExcelUniDto { campusName = uniCampName, uniId = uniId });
                    }
                }
            }
            return campuses;
        }
        public async Task<List<Course>> ReadCoursesExcelFile(IFormFile file)
        {
            var courses = new List<Course>();
            var filePath = GetMainDirectoryPath() + GetUniqueName(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.First();
                    var noOfRow = workSheet.Dimension.Rows;
                    var noOfCol = workSheet.Dimension.Columns;

                    int courseIndex = 0;
                    int stateIndex = 0;
                    int campusIndex = 0;
                    int disiplineIndex = 0;
                    int levelIndex = 0;
                    int feeIndex = 0;
                    int intakeIndex = 0;
                    int modeIndex = 0;
                    int durationIndex = 0;
                    int scholarshipIndex = 0;
                    int deadlineIndex = 0;
                    int overviewIndex = 0;
                    int learningOutcomesIndex = 0;
                    int startDateAndPriceIndex = 0;
                    int howToApplyIndex = 0;
                    int reviewsAndRankingsIndex = 0;

                    string value = "";

                    string stateValue = "";
                    string stateValueType = "";
                    string campusValue = "";
                    string campusValueType = "";
                    string disiplineValue = "";
                    string disiplineValueType = "";
                    string LevelValue = "";
                    string LevelValueType = "";

                    string courseName = "";
                    //string level = "";
                    string tutionFee = "";
                    string intake = "";
                    string mode = "";
                    string duration = "";
                    string scholarship = "";
                    DateTime applicationDeadline = default(DateTime);
                    string overview = "";
                    string learningOutcomes = "";
                    string startDateAndPrice = "";
                    string howToApply = "";
                    string reviewsAndRankings = "";

                    var dateTime = PKDateTimeZone.getDate();
                    CultureInfo provider = CultureInfo.InvariantCulture;

                    int stateId = 0;
                    int campusId = 0;
                    int disiplineId = 0;
                    int levelId = 0;

                    DateTime localdt;
                    for (int i = 1; i <= noOfRow; i++)
                    {
                        if (courseIndex == 0
                            && stateIndex == 0
                            && campusIndex == 0
                            && disiplineIndex == 0
                            && levelIndex == 0
                            && feeIndex == 0
                            && intakeIndex == 0
                            && modeIndex == 0
                            && durationIndex == 0
                            && scholarshipIndex == 0
                            && deadlineIndex == 0
                            && overviewIndex == 0
                            && learningOutcomesIndex == 0
                            && startDateAndPriceIndex == 0
                            && howToApplyIndex == 0
                            && reviewsAndRankingsIndex == 0
                            )
                        {
                            for (int j = 1; j <= noOfCol; j++)
                            {
                                value = workSheet.Cells[i, j].Value?.ToString().ToLower().Trim();
                                if (GetStateIndex(value, true))
                                    stateIndex = j;
                                else if (GetCampusIndex(value, true))
                                    campusIndex = j;
                                else if (GetDisiplineIndex(value, true))
                                    disiplineIndex = j;
                                else if (GetCourseIndex(value))
                                    courseIndex = j;
                                else if (GetLevelIndex(value, true))
                                    levelIndex = j;
                                else if (GetTutionFeeIndex(value))
                                    feeIndex = j;
                                else if (GetCourseIntakeIndex(value))
                                    intakeIndex = j;
                                else if (GetModeIndex(value))
                                    modeIndex = j;
                                else if (GetDurationIndex(value))
                                    durationIndex = j;
                                else if (GetScholarshipIndex(value))
                                    scholarshipIndex = j;
                                else if (GetApplicationDeadlineIndex(value))
                                    deadlineIndex = j;
                                else if (GetOverviewIndex(value))
                                    overviewIndex = j;
                                else if (GetLearningOutcomesIndex(value))
                                    learningOutcomesIndex = j;
                                else if (GetStartDateAndPriceIndex(value))
                                    startDateAndPriceIndex = j;
                                else if (GetHowToApplyIndex(value))
                                    howToApplyIndex = j;
                                else if (GetReviewsAndRankingsIndex(value))
                                    reviewsAndRankingsIndex = j;


                                if (courseIndex > 0
                                    && stateIndex > 0
                                    && campusIndex > 0
                                    && disiplineIndex > 0
                                    && levelIndex > 0
                                    && feeIndex > 0
                                    && intakeIndex > 0
                                    && modeIndex > 0
                                    && durationIndex > 0
                                    && scholarshipIndex > 0
                                    && deadlineIndex > 0
                                    && overviewIndex > 0
                                    && learningOutcomesIndex > 0
                                    && startDateAndPriceIndex > 0
                                    && howToApplyIndex > 0
                                    && reviewsAndRankingsIndex > 0
                                    )
                                    break;
                            }
                            continue;
                        }

                        stateValue = workSheet.Cells[i, stateIndex].Value?.ToString().Trim();
                        stateValueType = workSheet.Cells[i, stateIndex].Value?.GetType().ToString();
                        campusValue = workSheet.Cells[i, campusIndex].Value?.ToString().Trim();
                        campusValueType = workSheet.Cells[i, campusIndex].Value?.GetType().ToString();
                        disiplineValue = workSheet.Cells[i, disiplineIndex].Value?.ToString().Trim();
                        disiplineValueType = workSheet.Cells[i, disiplineIndex].Value?.GetType().ToString();

                        courseName = workSheet.Cells[i, courseIndex].Value?.ToString().Trim();

                        LevelValue = workSheet.Cells[i, levelIndex].Value?.ToString().Trim();
                        LevelValueType = workSheet.Cells[i, levelIndex].Value?.GetType().ToString();

                       
                        //level = workSheet.Cells[i, levelIndex].Value?.ToString().Trim();
                        tutionFee = workSheet.Cells[i, feeIndex].Value?.ToString().Trim();
                          
                        intake = workSheet.Cells[i, intakeIndex].Value?.ToString().Trim();
                        //value = workSheet.Cells[i, intakeIndex].Value?.GetType().ToString();
                        mode = workSheet.Cells[i, modeIndex].Value?.ToString().Trim();
                        duration = workSheet.Cells[i, durationIndex].Value?.ToString().Trim();
                        scholarship = workSheet.Cells[i, scholarshipIndex].Value?.ToString().Trim();
                        try
                        {
                            
                            applicationDeadline = default(DateTime);
                            value = workSheet.Cells[i, deadlineIndex].Value?.ToString().Trim();
                            if (GetApplicationDeadlineIndex(value)) continue;
                            if (!string.IsNullOrEmpty(value))
                            {
                                DateTime.TryParse(value, out applicationDeadline);
                                //double dateInDouble = double.Parse(value);
                                //applicationDeadline = DateTime.FromOADate(dateInDouble);//.ToString("MMMM dd, yyyy");
                            }
                                
                            //applicationDeadline = DateTime.ParseExact(value, new string[] { "MM/dd/yyyy", "MM-dd-yyyy", "MM.dd.yyyy" }, provider);

                        }
                        catch(Exception e)
                        {
                            var ex = e.Message;
                        }
                        overview = workSheet.Cells[i, overviewIndex].Value?.ToString().Trim();
                        learningOutcomes = workSheet.Cells[i, learningOutcomesIndex].Value?.ToString().Trim();
                        startDateAndPrice = workSheet.Cells[i, startDateAndPriceIndex].Value?.ToString().Trim();
                        howToApply = workSheet.Cells[i, howToApplyIndex].Value?.ToString().Trim();
                        reviewsAndRankings = workSheet.Cells[i, reviewsAndRankingsIndex].Value?.ToString().Trim();

                        if (string.IsNullOrEmpty(stateValue)
                            || string.IsNullOrEmpty(campusValue)
                            || string.IsNullOrEmpty(LevelValue)
                            || string.IsNullOrEmpty(tutionFee)
                            //|| string.IsNullOrEmpty(intake)
                            || string.IsNullOrEmpty(duration)
                            //|| string.IsNullOrEmpty(scholarship)
                            //|| string.IsNullOrEmpty(applicationDeadline)
                            ) continue;
                        if (stateValueType.Contains("String")
                            || campusValueType.Contains("String")
                            || disiplineValueType.Contains("String") 
                            || GetCourseIndex(courseName.ToLower()) || GetCourseIndex(courseName.ToLower(), true) 
                            || LevelValueType.Contains("String")
                            || GetTutionFeeIndex(tutionFee.ToLower()) 
                            || string.IsNullOrEmpty(intake) ? false : GetCourseIntakeIndex(intake.ToLower()) 
                            || GetDurationIndex(duration.ToLower()) 
                            || GetApplicationDeadlineIndex(applicationDeadline.ToString().ToLower()) 
                            || string.IsNullOrEmpty(scholarship) ? false : GetScholarshipIndex(scholarship.ToLower()) 
                            || string.IsNullOrEmpty(overview) ? false : GetOverviewIndex(overview.ToLower()) 
                            || string.IsNullOrEmpty(learningOutcomes) ? false : GetLearningOutcomesIndex(learningOutcomes.ToLower()) 
                            || string.IsNullOrEmpty(startDateAndPrice) ? false : GetStartDateAndPriceIndex(startDateAndPrice.ToLower()) 
                            || string.IsNullOrEmpty(howToApply) ? false : GetHowToApplyIndex(howToApply.ToLower()) 
                            || string.IsNullOrEmpty(reviewsAndRankings) ? false : GetReviewsAndRankingsIndex(reviewsAndRankings.ToLower()) 
                            ) continue;
                        
                        
                        if (DateTime.TryParse(intake, out localdt))
                        {
                            intake = localdt.ToString("MMM-yy");
                        }
                        //if (!string.IsNullOrEmpty(value))
                        //{
                        //    double dateInDouble = double.Parse(value);
                        //    intake = DateTime.FromOADate(dateInDouble).ToString("MMM-yy");
                        //    //var newIntakeValue = DateTime.ParseExact(value, new string[] { "MM/dd/yyyy HH:mm:ss,fff", "MM-dd-yyyy HH:mm:ss,fff", "MM.dd.yyyy HH:mm:ss,fff" }, provider);
                        //    //intake = newIntakeValue.ToString("MM-dd-yyyy");
                        //}
                        stateId = Convert.ToInt32(stateValue);
                        campusId = Convert.ToInt32(campusValue);
                        disiplineId = Convert.ToInt32(disiplineValue);
                        levelId = Convert.ToInt32(LevelValue);
                        if (stateId <= 0 && campusId<=0 && disiplineId<=0 && levelId<=0) continue;
                        courses.Add(new Course {
                            DisiplineId = disiplineId,
                            CampusId = campusId,
                            StateId = stateId,
                            CourseName = courseName,
                            Program_Id = levelId,
                            CourseTutionFee = tutionFee,
                            CourseMode = mode,
                            CourseDuration = duration,
                            CourseIntake = intake ?? "",
                            CourseScholarship = scholarship ?? "",
                            CourseApplicationDeadline = applicationDeadline,
                            CourseOverview = overview ?? "",
                            CourseLearningOutcome = learningOutcomes ?? "",
                            CourseStartDateAndPrice = startDateAndPrice ?? "",
                            CourseHowToApply = howToApply ?? "",
                            CourseReviewAndRanking = reviewsAndRankings ?? ""
                    });
                        //courses.Add(new Course { CourseName = courseName,  DisiplineId = uniId });
                    }
                }
            }
            return courses;
        }
        private void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        } 
        private string GetUniqueName(string name)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next() + Guid.NewGuid() + name;
        }
        private string GetMainDirectoryPath()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            CreateDirectory(path);
            path = Path.Combine(path, "UploadExcelFiles\\");
            CreateDirectory(path);
            return path;
        }
        private bool GetCountryIndex(string name, bool countryIdFlag = false)
        {
            if(string.IsNullOrEmpty(name))
                return false;
            if (countryIdFlag)
            {
                if (name == "country-id")
                    return true;
            }else if (name == "country-name")
                return true;
            return false;
        }
        private bool GetStateIndex(string name, bool stateIdFlag = false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (stateIdFlag)
            {
                if (name == "state-id")
                    return true;
            }else if (name == "state-name")
                return true;
            return false;
        }
        private bool GetCampusIndex(string name, bool campusFlag = false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (campusFlag)
            {
                if (name == "campus-id")
                    return true;
            }
            else if(name == "campus-name")
                return true;
            return false;
        }
        private bool GetUniIndex(string name, bool uniIdFlag= false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (uniIdFlag)
            {
                if (name == "university-id")
                    return true;
            }
            else if (name == "university-name")
                return true;
            return false;
        }
        private bool GetDisiplineIndex(string name, bool disipFlag = false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (disipFlag)
            {
                if (name == "disipline-id")
                    return true;
            }
            else if (name == "disipline-name")
                return true;
            return false;
        }
        private bool GetCourseIndex(string name, bool courseFlag = false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (courseFlag)
            {
                if (name == "course-id")
                    return true;
            }
            else if (name == "course-name")
                return true;
            return false;
        }
        private bool GetCourseIntakeIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "intake")
                return true;
            return false;
        }
        private bool GetLevelIndex(string name, bool LevelFlag = false)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (LevelFlag)
            {
                if (name == "level-id")
                    return true;
            }
            else if (name == "level-name")
                return true;      
            return false;
        }
        private bool GetTutionFeeIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "tution-fee")
                return true;
            return false;
        }
        private bool GetModeIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "mode")
                return true;
            return false;
        }
        private bool GetDurationIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "duration")
                return true;
            return false;
        }
        private bool GetScholarshipIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "scholarship%")
                return true;
            return false;
        }
        private bool GetApplicationDeadlineIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "application-deadline")
                return true;
            return false;
        }
        private bool GetOverviewIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "overview")
                return true;
            return false;
        }
        private bool GetLearningOutcomesIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "learning-outcomes")
                return true;
            return false;
        }
        private bool GetStartDateAndPriceIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "start-date-and-price")
                return true;
            return false;
        }  
        private bool GetHowToApplyIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "how-to-apply")
                return true;
            return false;
        } 
        private bool GetReviewsAndRankingsIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else if (name == "reviews-and-rankings")
                return true;
            return false;
        }
    }
}
