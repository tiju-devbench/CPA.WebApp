import { Component, OnInit } from '@angular/core';
import { ExamResults } from '../models/examresults';
import { PassedSubjects } from '../models/PassedSubjects';
import { ExamService } from '../services/exam.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  results:any;
  constructor(
    public examService: ExamService
  ) {}
  ngOnInit(): void {
    this.loadResults();
  }
  loadResults() {
    this.examService.loadPassedSubjects().subscribe(
      (next: PassedSubjects) => {
          this.results = next;
      },
      (error) => {
        console.log(error);
      }
    );;
  }
}
