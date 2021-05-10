import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [AccountService]
})
export class LoginComponent {
  userName: string;
  password: string;
  incorrect = false;

  constructor(http: HttpClient, public accountService: AccountService, private router: Router) {

  }

  login() {
    this.incorrect = false;
    this.accountService.login({ userName: this.userName, password: this.password }).subscribe((result: any) => {
      if (result.response) {
        this.incorrect = false;
        this.router.navigate(['/authentication/login']);
      }
      else {
        this.incorrect = true;
      }
    }, error => {
      console.log(error);
      this.incorrect = true;
    });
  }
}
