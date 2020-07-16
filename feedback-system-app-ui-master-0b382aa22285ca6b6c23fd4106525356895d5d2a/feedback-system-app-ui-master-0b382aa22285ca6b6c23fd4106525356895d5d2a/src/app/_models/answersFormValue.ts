export class AnswersFormValue{
    radioAns: string;
    multipleAnswer?: boolean;
    freeTextAnswer?: boolean;
    customQuestion?: boolean;
    questionTextArea: string;
    answerArray: {answerTextArea: string}[];
    selectedAnswer: number;
}
