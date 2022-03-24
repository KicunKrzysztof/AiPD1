import { Component, OnChanges, Input } from '@angular/core';
import { AnalysisResponse } from 'src/app/models/analysisResponse';
import { PeriodicElement } from '../../models/periodic-element';

@Component({
  selector: 'app-detail-table',
  templateUrl: './detail-table.component.html',
  styleUrls: ['./detail-table.component.css']
})
export class DetailTableComponent implements OnChanges {
  constructor() { }
  tableData: PeriodicElement[] = [];
  displayedColumns: string[] = ['parameter', 'value'];
  @Input() data: AnalysisResponse;

  ngOnChanges(){
    this.tableData = [
      {parameter: 'VDR', value: this.data.vdr},
      {parameter: 'VSTD', value: this.data.vstd},
      {parameter: 'ZSDT', value: this.data.zstd},
      {parameter: 'HZCRR', value: this.data.hzcrr},
      {parameter: 'LSTER', value: this.data.lster},
    ];
  }
}
