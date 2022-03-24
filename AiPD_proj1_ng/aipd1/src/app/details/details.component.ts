import { Component, OnInit } from '@angular/core';
import { DataStorageService } from '../data-storage.service';
import { ChartDataService } from '../chart-data.service';
import { ActivatedRoute } from '@angular/router';
import { AnalysisResponse } from '../models/analysisResponse';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  constructor(private dataStorageService: DataStorageService,
    private route: ActivatedRoute,
    private http: ChartDataService) { }
  id:number;
  data:AnalysisResponse;
  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.data = this.dataStorageService.getData(this.id);
  }
  save():void{
    this.http.saveCSV(this.data.filename).subscribe();
  }
}
