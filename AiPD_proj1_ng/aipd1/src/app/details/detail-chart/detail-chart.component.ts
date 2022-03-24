import { Component, OnChanges, Input } from '@angular/core';
import { ChartType } from "angular-google-charts";

@Component({
  selector: 'app-detail-chart',
  templateUrl: './detail-chart.component.html',
  styleUrls: ['./detail-chart.component.css']
})
export class DetailChartComponent implements OnChanges {
  type: ChartType = ChartType.LineChart;
  data = [[0, 0]];
  options = {legend: {position: 'none'}, pointSize: 1, enableInteractivity: false, explorer: {
    maxZoomOut:2,
    maxZoomIn: 8,
    keepInBounds: true
}};
  cols = ["time", "signal"];
  width = 700;
  height = 350;
  @Input() chartData: number[][];
  @Input() title: string;

  constructor() {  }
  ngOnChanges(): void{
    if (this.chartData.length != 0)
      this.data = this.chartData;
  }
}
