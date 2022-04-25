import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Currency } from '../models/currency.model';
import { ExchangeRate, ExchangeRateRow } from '../models/exchange-rate.model';

@Injectable()
export class ExchangeRateService {
  urlService: string;
  constructor(private httpClient: HttpClient) {
    this.urlService = environment.urlService;
  }
  getExchangeRates(): Observable<ExchangeRateRow[]> {
    return this.httpClient.get<ExchangeRateRow[]>(this.urlService + 'currencyexchangerate/all');
  }
  upsertExchangeRate(request: ExchangeRate): Observable<Currency[]> {
    return this.httpClient.put<any>(
      this.urlService + 'currencyexchangerate',
      request
    );
  }
}
