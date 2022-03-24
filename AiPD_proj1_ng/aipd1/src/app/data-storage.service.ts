import { Injectable } from '@angular/core';
import { AnalysisResponse } from './models/analysisResponse';
import { ChartDataService } from './chart-data.service';

@Injectable({
  providedIn: 'root'
})
export class DataStorageService {
  constructor(private http: ChartDataService) { }
  charts: number[] = [];
  chartsAnalysis: AnalysisResponse[] = [];
  chartsequence: number = 0;
  emptyAnalysis: AnalysisResponse = {
    filename:"",
    volume: [],
    ste:[],
    zcr:[],
    sr:[],
    chart:[],
    vstd:0,
    vdr:0,
    lster:0,
    zstd:0,
    hzcrr:0
  }
  remove(i: number): void {
    this.charts.splice(i, 1);
    this.chartsAnalysis.splice(i, 1);
  }
  add(): void {
    this.charts.push(this.chartsequence);
    this.chartsAnalysis.push(this.emptyAnalysis);
    this.chartsequence++;
  }
  newFileName(filename: string, i:number):void{
    this.http.getAnalysis(filename).subscribe((analysis: AnalysisResponse) => {
      this.chartsAnalysis[i] = analysis;
    })
  }
  getData(id: number):AnalysisResponse{
    var index: number = 0;
    for(; index < this.charts.length; index++){
      if (this.charts[index] === id)
        break;
    }
    return this.chartsAnalysis[index];
  }
}
