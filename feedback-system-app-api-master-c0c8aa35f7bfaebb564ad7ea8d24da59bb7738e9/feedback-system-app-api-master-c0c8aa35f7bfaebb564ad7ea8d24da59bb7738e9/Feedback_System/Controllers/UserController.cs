using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback_System.Models;
using Feedback_System.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback_System.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/user")]
    [ApiController]
    public class UserController
    {
        private readonly IUserService userService;
        private readonly IExcelUploadService excelService;
        public UserController(IUserService _userService, IExcelUploadService _excelService)
        {
            userService = _userService;
            excelService = _excelService;
        }

        [HttpGet]
        //[Route("/getUserData")]
        public ActionResult<List<UserModel>> GetUserData()
        {
            return userService.GetUserData();
        }

        [HttpGet]
        [Route("getLoggedInUserData/{id?}/{password?}")]
        public ActionResult<UserModel> GetLoggedInUserData(string id = "727775", string password = "")
        {
            var result = userService.GetLoggedInUserData(id, password);
            if (result != null)
                return result;
            else
                return result;
        }

        [HttpGet]
        [Route("getQuestionData")]
        public ActionResult<List<QuestionModel>> GetQuestionData()
        {
            var result = userService.GetQuestionData();
            if (result != null)
                return result;
            else
                return result;
        }
        [HttpPost]
        [Route("updateQuestionData/{index}")]
        public ActionResult<List<QuestionModel>> UpdateQuestionData(int index, [FromBody] QuestionModel updateQuestion)
        {
            var result = userService.UpdateQuestionData(index, updateQuestion);
            if (result != null)
                return result;
            else
                return result;
        }

        [HttpPost]
        [Route("postQuestionData")]
        public ActionResult<List<QuestionModel>> PostQuestionData([FromBody] AnswersFormValue newAnswer)
        {
            QuestionModel newQuestion = new QuestionModel();
            newQuestion.Answers = newAnswer;
            newQuestion.Questions = newAnswer.questionTextArea;
            newQuestion.FBType = "random";
            newQuestion.TotalAnswers = newAnswer.answerArray.Count();
            var result = userService.PostQuestionData(newQuestion);
            if (result != null)
                return result;
            else
                return result;
        }

        [HttpPost]
        [Route("deleteQuestionData")]
        public ActionResult<List<QuestionModel>> DeleteQuestionData([FromBody] int index)
        {
            var result = userService.DeleteQuestionData(index);
            if (result != null)
                return result;
            else
                return result;

        }

        [HttpPost]
        [Route("postFeedbackData")]
        public ActionResult<bool> PostFeedbackData([FromBody] List<FeedbackModel> feedbackList)
        {
            userService.PostFeedbackData(feedbackList);
            return true;
        }

    }


}