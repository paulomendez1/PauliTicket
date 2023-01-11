import { NgModule } from '@angular/core';
import { BrowserModule, Title } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing-module';
import { HomeComponent } from './components/home/home.component';
import { MaterialModule } from './material/material/material.module';
import { LayoutModule } from '@angular/cdk/layout';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectAllComponent } from './utilities/select-all/select-all.component';
import { AuthFormComponent } from './utilities/auth-form/auth-form.component';
import { DisplayErrorsComponent } from './utilities/display-errors/display-errors.component';
import { AuthService } from './services/auth.service';
import { JwtInterceptorService } from './utilities/jwt-interceptor/jwt-interceptor.service';
import { RequireAuthenticatedUserRouteGuard } from './utilities/auth-guard/require.guard';
import { EventDetailComponent } from './components/event-detail/event-detail.component';
import { LoginComponent } from './components/security/login/login.component';
import { RegisterComponent } from './components/security/register/register.component';
import { MarkdownModule } from 'ngx-markdown';
import { OrderDialogComponent } from './components/order-dialog/order-dialog.component';
import { OrderDetailComponent } from './components/order-detail/order-detail.component';
import { ProfileComponent } from './components/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SelectAllComponent,
    LoginComponent,
    AuthFormComponent,
    DisplayErrorsComponent,
    RegisterComponent,
    EventDetailComponent,
    OrderDialogComponent,
    OrderDetailComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    AppRoutingModule,
    MaterialModule,
    LayoutModule,
    NgbModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MarkdownModule
  ],
  providers: [
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true
    },
    RequireAuthenticatedUserRouteGuard,
    Title
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
