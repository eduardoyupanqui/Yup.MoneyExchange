import { Component } from '@angular/core';
import { Event, NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'yup-money-exchange';

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    if(!sessionStorage.getItem('token')){
      this.router.navigate(['login']);
   }
  }
}
