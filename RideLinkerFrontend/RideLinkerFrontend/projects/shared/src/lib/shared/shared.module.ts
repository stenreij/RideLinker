import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../header/header.component';
import { FooterComponent } from '../footer/footer.component';
import { RouterLink, RouterModule } from '@angular/router';
import { LoginComponent } from '../auth/login/login.component';
import { AuthService } from '../auth/auth.service';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from '../auth/register/register.component';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
  ],
  providers: [AuthService],
  exports: [HeaderComponent, FooterComponent, LoginComponent, RegisterComponent]
})
export class SharedModule { }
