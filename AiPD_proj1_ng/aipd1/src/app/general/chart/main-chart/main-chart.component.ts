import { Component, OnChanges, Input } from '@angular/core';
import { ChartType } from "angular-google-charts";

@Component({
  selector: 'app-main-chart',
  templateUrl: './main-chart.component.html',
  styleUrls: ['./main-chart.component.css']
})
export class MainChartComponent implements OnChanges {
  type: ChartType = ChartType.LineChart;
  data = [[0, 0]];
  options = {legend: {position: 'none'}, pointSize: 1, enableInteractivity: false, explorer: {
    maxZoomOut:2,
    maxZoomIn: 8,
    keepInBounds: true
}};
  cols = ["time", "signal"];
  width = 1000;
  height = 350;
  @Input() chartData: number[][];

  constructor() {  }
  ngOnChanges(): void{
    if (this.chartData.length != 0)
      this.data = this.chartData;
  }
}