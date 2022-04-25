import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { ExchangeRateComponent } from './container/exchange-rate.component';

const routes: Routes = [
  { path: '', component: ExchangeRateComponent, canActivate:[AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExchangeRateRoutingModule { }
