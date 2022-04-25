import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Currency } from '../models/currency.model';
import { ExchangeRate } from '../models/exchange-rate.model';

@Injectable()
export class ExchangeService {

  urlService: string;
  constructor(private httpClient: HttpClient) 
  {
    this.urlService = environment.urlService
  }
  getCurrencies(): Observable<Currency[]>
  {
    return this.httpClient.get<Currency[]>(this.urlService + 'currency/all');
  }
  makeExchange(request: ExchangeRate): Observable<any> {
    return this.httpClient.post<any>(this.urlService + 'exchange', request)
  }
}
