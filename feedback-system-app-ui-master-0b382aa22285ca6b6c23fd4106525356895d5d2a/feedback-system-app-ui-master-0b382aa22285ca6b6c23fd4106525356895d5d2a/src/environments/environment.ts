// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  
  userdataURL: 'http://localhost:54414/api/user',
  questionsGetURL: 'http://localhost:54414/api/user/GetQuestionData',
  questionsPostURL: 'http://localhost:54414/api/user/PostQuestionData',
  questionsUpdateURL: 'http://localhost:54414/api/user/UpdateQuestionData',
  questionsDeleteURL: 'http://localhost:54414/api/user/DeleteQuestionData',
  eventGetUrl: 'http://localhost:54414/api/event/getEventsData',
  participantGetUrl: 'http://localhost:54414/api/event/getParticipantsData',
  participantUpdateUrl: 'http://localhost:54414/api/event/updateParticipantsData',
  buMasterUrl: 'http://localhost:54414/api/master/getBUMasterData',
  getParticipantByIdUrl: 'http://localhost:54414/api/event/getParticipantsDataById',
  getEventbyIdUrl: 'http://localhost:54414/api/event/getEventDataById',
  feedbackPostURL: 'http://localhost:54414/api/user/PostFeedbackData',
  excelUploadURL: 'http://localhost:54414/api/fileupload/test',
  getParticipantByUserRoleUrl: 'http://localhost:54414/api/event/getParticipantsDataByRoleUser',
  getEventbyUserRoleUrl: 'http://localhost:54414/api/event/getEventDataByRoleUser',
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
