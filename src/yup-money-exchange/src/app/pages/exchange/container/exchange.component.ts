import { Component, OnInit } from '@angular/core';
import { Currency } from '../models/currency.model';
import { ExchangeRate } from '../models/exchange-rate.model';
import { ExchangeService } from '../services/exchange.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-exchange',
  templateUrl: './exchange.component.html',
  styleUrls: ['./exchange.component.scss']
})
export class ExchangeComponent implements OnInit {
  
  success: boolean = false;
  currencies: Currency[] = [];
  currencyFrom: Currency = new Currency();
  currencyTo: Currency = new Currency();

  exchangeRate: ExchangeRate = new ExchangeRate();
  constructor(private exchangeService: ExchangeService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.exchangeService.getCurrencies().subscribe({
      next: (response) => this.currencies = response
    });
  }
  
  makeExchangeRate(){
    this.exchangeRate.currencyFromId = this.currencyFrom.id;
    this.exchangeRate.currencyToId = this.currencyTo.id;
    this.exchangeService.makeExchange(this.exchangeRate).subscribe({
      next: (response)=> {
        this.success = response.success;
        if (response.success){
          this.exchangeRate.exchangeRate = response.dataObject.exchangeRate;
          this.exchangeRate.amountExchange = response.dataObject.amountExchange;
        }else {
          this.snackBar.open(response.messages[0].message, undefined, {
            duration: 2000,
          });
        }
      }
    });
  }

  changeCurrency(currency: Currency)
  {
  }
}
