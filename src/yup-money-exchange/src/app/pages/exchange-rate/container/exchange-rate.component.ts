import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Currency } from '../models/currency.model';
import { ExchangeRate, ExchangeRateRow } from '../models/exchange-rate.model';
import { CurrencyService } from '../services/currency.service';
import { ExchangeRateService } from '../services/exchange-rate.service';

@Component({
  selector: 'app-exchange',
  templateUrl: './exchange-rate.component.html',
  styleUrls: ['./exchange-rate.component.scss']
})
export class ExchangeRateComponent implements OnInit {
  success: boolean = false;
  currencies: Currency[] = [];
  currencyFrom: Currency = new Currency();
  currencyTo: Currency = new Currency();

  exchangeRate: ExchangeRate = new ExchangeRate();
  displayedColumns: string[] = ['position', 'currencyFrom', 'currencyTo', 'exchangeRate'];
  exchangeRates: ExchangeRateRow[] = [];
  constructor(
    private currencyService: CurrencyService,
    private exchangeRateService: ExchangeRateService,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.currencyService.getCurrencies().subscribe({
      next: (response) => this.currencies = response
    });
    this.getAllExchageRate();
  }

  changeCurrency(currency: Currency){
    if(this.currencyFrom.id !== this.currencyTo.id) {
      this.exchangeRate.exchange = 0;
    } else {
      this.exchangeRate.exchange = 1;
    }
  }

  updateExchangeRate(){
    this.exchangeRate.currencyFromId = this.currencyFrom.id;
    this.exchangeRate.currencyToId = this.currencyTo.id;
    this.exchangeRateService.upsertExchangeRate(this.exchangeRate).subscribe({
      next: (response: any) => {
        this.success = response.success;
        if (response.success){
          this.snackBar.open("Se guardó correctamente.", undefined, {
            duration: 2000,
          });
          this.getAllExchageRate();
        }else {
          this.snackBar.open(response.messages[0].message, undefined, {
            duration: 2000,
          });
        }
      }
    });
  }

  getAllExchageRate(){
    this.exchangeRateService.getExchangeRates().subscribe({
      next: (response) => this.exchangeRates = response
    });
  }
}
