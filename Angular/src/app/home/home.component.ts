import { Component, OnInit } from '@angular/core';
import{Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loggedUser:any;
  isLoggedIn:boolean=false;
  isAllUersEnable:boolean=false;
  constructor(private router:Router) { }

  ngOnInit(): void { 
  this.loggedUser=localStorage.getItem('userId')?.toString();
  this.isLoggedIn=(localStorage.getItem('loggedStatus')?.toString()=="true");
  if(this.loggedUser==='admin')
    this.isAllUersEnable=true;
  else
    this.isAllUersEnable=false;
   
}
logIn()
{
  this.loggedUser=localStorage.getItem('userId')?.toString();
  if(this.loggedUser!=null)
  {
    this.logoff();
  }
  this.router.navigateByUrl('Login');
}

logoff()
{
  localStorage.removeItem('userId');
  localStorage.removeItem('loggedStatus');
  this.router.navigateByUrl('home');
  this.ngOnInit();
}
}
