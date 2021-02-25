import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExamService {

  baseUrl = environment.apiUrl + 'exam/';

  constructor(private http: HttpClient) { }

  loadResults(){
    return this.http.get(this.baseUrl + 'results');
  }

  loadPassedSubjects(){
    return this.http.get(this.baseUrl + 'LoadPassedSubjects');
  }
}
