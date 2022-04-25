export class ExchangeRate {
    currencyFromId: string;
    currencyToId: string;
    exchange: number;

    constructor(){
        this.currencyFromId = '';
        this.currencyToId = '';
        this.exchange = 0;
    }
}

export class ExchangeRateRow {
    position: number;
    currencyFromId: string;
    currencyFrom: string;
    currencyToId: string;
    currencyTo: string;
    exchangeRate: number;

    constructor(){
        this.position = 0;
        this.currencyFromId = '';
        this.currencyFrom = '';
        this.currencyToId = '';
        this.currencyTo = '';
        this.exchangeRate = 0;
    }
}