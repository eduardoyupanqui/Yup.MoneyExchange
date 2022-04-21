import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  user: string = '';
  password: string = '';
  constructor(
    private router: Router,
    private loginservice: LoginService
  ) {}

  ngOnInit(): void {}
  login() {
    this.loginservice.login(this.user, this.password).subscribe({
      next: (data) => {
        this.router.navigate(['']);
      },
      error: (error) => {}}
    );
  }
}
