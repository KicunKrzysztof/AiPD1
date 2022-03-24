import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainChartComponent } from './general/chart/main-chart/main-chart.component';
import { HttpClientModule } from '@angular/common/http';
import { GoogleChartsModule } from 'angular-google-charts';
import { AutocompleteComponent } from './general/chart/autocomplete/autocomplete.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AngularMaterialModule } from './angular-material.module';
import { MatCardModule } from '@angular/material/card';
import { ChartComponent } from './general/chart/chart.component';
import { FormsModule }   from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { DetailsComponent } from './details/details.component';
import { GeneralComponent } from './general/general.component';
import { DetailChartComponent } from './details/detail-chart/detail-chart.component';
import { DetailTableComponent } from './details/detail-table/detail-table.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    MainChartComponent,
    AutocompleteComponent,
    ChartComponent,
    DetailsComponent,
    GeneralComponent,
    DetailChartComponent,
    DetailTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    GoogleChartsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    AngularMaterialModule,
    MatCardModule,
    FormsModule,
    MatTableModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
