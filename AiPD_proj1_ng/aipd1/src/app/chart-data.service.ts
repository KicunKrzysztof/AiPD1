import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AnalysisResponse } from './models/analysisResponse';

@Injectable({
  providedIn: 'root'
})
export class ChartDataService {
  constructor(private http: HttpClient) { }

  url = 'http://localhost:27764/api/main';
  getFiles(){
    return this.http.get<string[]>(this.url + '/files')
  }
  getAnalysis(filename: string): Observable<AnalysisResponse>{
    return this.http.post<AnalysisResponse>(this.url + '/analysis', {name: filename});
  }
  saveCSV(filename: string):Observable<boolean>{
    return this.http.post<boolean>(this.url + '/save', {name: filename});
  }
}
