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