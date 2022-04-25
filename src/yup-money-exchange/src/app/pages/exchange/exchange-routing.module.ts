import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { ExchangeComponent } from './container/exchange.component';

const routes: Routes = [
  { path: '', component: ExchangeComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExchangeRoutingModule { }
