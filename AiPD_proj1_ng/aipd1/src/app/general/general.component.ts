import { Component, OnInit} from '@angular/core';
import { ChartDataService } from '../chart-data.service';
import { DataStorageService } from '../data-storage.service';
import { ChartMsg } from '../models/chartMsg';
import { AnalysisResponse } from '../models/analysisResponse';

@Component({
  selector: 'app-general',
  templateUrl: './general.component.html',
  styleUrls: ['./general.component.css']
})
export class GeneralComponent implements OnInit {
  constructor(private http:ChartDataService,
    private dataStorage: DataStorageService){}
    
  charts: number[] = [];
  chartsAnalysis: AnalysisResponse[] = [];
  
  ngOnInit(): void {
    this.charts = this.dataStorage.charts;
    this.chartsAnalysis = this.dataStorage.chartsAnalysis
  }

  remove(val: number): void {
    this.dataStorage.remove(this.getRealChartIndex(val));
    this.charts = this.dataStorage.charts;
    this.chartsAnalysis = this.dataStorage.chartsAnalysis
  }
  getRealChartIndex(val: number):number{
    var index: number = 0;
    for(let element of this.charts){
      if (element === val)
        break;
      index++;
    }
    return index;
  }
  add(): void {
    this.dataStorage.add();
    this.charts = this.dataStorage.charts;
    this.chartsAnalysis = this.dataStorage.chartsAnalysis
  }
  newFileName(msg: ChartMsg):void{
    this.dataStorage.newFileName(msg.filename, this.getRealChartIndex(msg.chartIndex))
    this.chartsAnalysis = this.dataStorage.chartsAnalysis
  }
}