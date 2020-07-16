using Feedback_System.Entities;
using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Mappers
{
    public class QuestionMapper
    {
        public List<QuestionModel> MapUserEntityToQuestionModel(List<Questions> questionEntity)
        {
            List<QuestionModel> questionModelList = new List<QuestionModel>();
            questionEntity.ForEach(question =>
            {
                QuestionModel questionModel = new QuestionModel();
                questionModel.FBType = question.Fbtype;
                questionModel.Q_ID = question.QId;
                questionModel.TotalAnswers = question.TotalAnswers;
                questionModel.Questions = question.QuestionTextArea;
                AnswersFormValue answers = new AnswersFormValue();
                answers.radioAns = question.RadioAnswer;
                answers.multipleAnswer = question.IsMultipleAnswer;
                answers.freeTextAnswer = question.IsFreeTextAnswer;
                answers.customQuestion = question.IsCustomQuestion;
                answers.questionTextArea = question.QuestionTextArea;
                List<AnswerArray> answerArrayList = new List<AnswerArray>();
                List<Answers> answers_db = question.Answers.ToList();
                answers_db.ForEach(answer =>
                {
                    AnswerArray answerArray = new AnswerArray();
                    answerArray.answerTextArea = answer.AnswerTextArea;
                    answerArrayList.Add(answerArray);
                });
                answers.answerArray = answerArrayList;
                questionModel.Answers = answers;
                questionModelList.Add(questionModel);
            });
            return questionModelList;
        }

        public Questions MapQuestionModelToEntity(QuestionModel questionModel)
        {
            Questions dbQuestions = new Questions();
            dbQuestions.QuestionTextArea = questionModel.Questions;
            dbQuestions.RadioAnswer = questionModel.Answers.radioAns;
            dbQuestions.IsCustomQuestion = questionModel.Answers.customQuestion ?? false;
            dbQuestions.IsFreeTextAnswer = questionModel.Answers.freeTextAnswer ?? false;
            dbQuestions.IsMultipleAnswer = questionModel.Answers.multipleAnswer ?? false;
            dbQuestions.TotalAnswers = questionModel.TotalAnswers ?? default(int);
            dbQuestions.Fbtype = questionModel.FBType;

            List<Answers> answers = new List<Answers>();

            List<AnswerArray> answersModel = questionModel.Answers.answerArray.ToList();
            answersModel.ForEach(answer => {
                Answers dbAnswer = new Answers();
                dbAnswer.AnswerTextArea = answer.answerTextArea;
                answers.Add(dbAnswer);
            });
            dbQuestions.Answers = answers;
            return dbQuestions;
        }

        public List<Feedback> MapFeedbackModelToEntity(List<FeedbackModel> feedbackList)
        {
            List<Feedback> dbFeedbacks = new List<Feedback>();
            feedbackList.ForEach(fb => {
                Feedback fbentity = new Feedback();
                fbentity.Guid = Guid.NewGuid();
                fbentity.AssociateId = fb.ParticipantId;
                fbentity.EventId = fb.EventId;
                fbentity.Question = fb.Question;
                fbentity.Answer = fb.Answer;
                fbentity.Qtype = 1;
                dbFeedbacks.Add(fbentity);
            });
            return dbFeedbacks;
        }

        //public List<Feedback> MapFeedbackEntityToModel(List<FeedbackModel> feedbackList)
        //{
        //    List<Feedback> dbFeedbacks = new List<Feedback>();
        //    feedbackList.ForEach(fb => {
        //        Feedback fbentity = new Feedback();
        //        fbentity.Guid = Guid.NewGuid();
        //        fbentity.AssociateId = fb.ParticipantId;
        //        fbentity.EventId = fb.EventId;
        //        fbentity.Question = fb.Question;
        //        fbentity.Answer = fb.Answer;
        //        fbentity.Qtype = 1;
        //        dbFeedbacks.Add(fbentity);
        //    });
        //    return dbFeedbacks;
        //}
    }

}
