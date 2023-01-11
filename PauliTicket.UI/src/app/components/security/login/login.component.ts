import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, Subject, take, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { userCredentials } from 'src/app/models/security'
import { parseWebAPIErrors } from 'src/app/utilities/shared.functions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private authService: AuthService,
    private router: Router) { }

  errors: string[] = []
  ngOnInit(): void {
  }

  login(userCredentials: userCredentials) {
    this.errors = [];
    this.authService.login(userCredentials).subscribe(authenticationResponse => {
      this.authService.saveToken(authenticationResponse)
      this.router.navigate(['/'])
    }, error => this.errors = parseWebAPIErrors(error))
  }
}
