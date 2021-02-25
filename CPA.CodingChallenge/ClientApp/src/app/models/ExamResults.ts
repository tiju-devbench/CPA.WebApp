export class ExamResults {
  public data : ExamResult[];
}

export class ExamResult {
  public subject : string;
  Results: Result[];
}

export class Result {
  public year : string;
  public grade : string;
}
