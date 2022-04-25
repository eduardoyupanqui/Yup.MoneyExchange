import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: 'exchange', loadChildren: () => import('./pages/exchange/exchange.module').then(m=> m.ExchangeModule)},
  { path: 'rate', loadChildren: ()=> import('./pages/exchange-rate/exchange-rate.module').then(m=> m.ExchangeRateModule)},
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
