import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DetailsComponent } from './details/details.component';
import { GeneralComponent } from './general/general.component';

const routes: Routes = [
  {path: 'details/:id', component: DetailsComponent},
  {path: '', component:GeneralComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
