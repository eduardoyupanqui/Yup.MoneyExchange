import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExchangeRoutingModule } from './exchange-routing.module';
import { ExchangeComponent } from './container/exchange.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ExchangeService } from './services/exchange.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';

var materialModules = [
  MatExpansionModule,
  MatButtonModule,
  MatFormFieldModule,
  MatInputModule,
  MatCardModule,
  MatSelectModule,
  MatOptionModule,
  MatInputModule,
  MatSnackBarModule
];
@NgModule({
  declarations: [
    ExchangeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule, 
    ExchangeRoutingModule,
    ...materialModules
  ],
  providers: [ExchangeService]
})
export class ExchangeModule { }
