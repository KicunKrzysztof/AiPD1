import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { AnalysisResponse } from '../../models/analysisResponse';
import { ChartMsg } from '../../models/chartMsg';
import { ChartDataService } from '../../chart-data.service';

export interface PeriodicElement {
  parameter: string;
  value: number;
}

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnChanges {
  constructor(private http: ChartDataService) { }

  @Input() chartIndex:number;
  @Input() data:AnalysisResponse;
  @Output() removeChart = new EventEmitter<number>();
  @Output() fileChange = new EventEmitter<ChartMsg>();
  title: string = "";
  tableData: PeriodicElement[] = [];
  displayedColumns: string[] = ['parameter', 'value'];

  remove(): void{
    this.removeChart.emit(this.chartIndex);
  }
  save():void{
    this.http.saveCSV(this.data.filename).subscribe(res => console.log(res));
  }
  newFilename(newFilename:string):void{
    var tmp:ChartMsg = {
      filename: newFilename,
      chartIndex: this.chartIndex
    }
    this.fileChange.emit(tmp);
  }
  ngOnChanges(){
    this.tableData = [
      {parameter: 'VDR', value: this.data.vdr},
      {parameter: 'ZCR', value: this.data.vstd},
      {parameter: 'ZSDT', value: this.data.zstd},
      {parameter: 'HZCRR', value: this.data.hzcrr},
      {parameter: 'LSTER', value: this.data.lster},
    ];
  }
}
