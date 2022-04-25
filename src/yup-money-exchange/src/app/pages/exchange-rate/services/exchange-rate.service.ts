import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Currency } from '../models/currency.model';
import { ExchangeRate } from '../models/exchange-rate.model';

@Injectable()
export class ExchangeRateService {
  urlService: string;
  constructor(private httpClient: HttpClient) {
    this.urlService = environment.urlService;
   }

  upsertExchangeRate(request: ExchangeRate): Observable<Currency[]>
  {
    return this.httpClient.put<any>(this.urlService + 'currencyexchangerate', request);
  }
}
