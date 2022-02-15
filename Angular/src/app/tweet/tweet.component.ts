import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import{Router} from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import{TweetappService} from './../tweetapp.service';
import {MatDialog, MatDialogConfig } from '@angular/material/dialog';

@Component({
  selector: 'app-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})
export class TweetComponent implements OnInit {
  tweets:any=[];
  loggedUserObject:any;
  loggedUser:any;
  isLoggedIn:boolean=false;
  Tweetmessage:string='';
  isEdit:boolean=false;
  tweetid:any;
  newTweet=new FormGroup({
   Message:new FormControl('')
  })
  tweet=new FormGroup({
    Reply_Message:new FormControl(''),
   })
   Tweetsearch=new FormGroup({
     searchInput:new FormControl('')
   })

  constructor(private service:TweetappService,private router:Router,private matdialog:MatDialog,private dynamicRef: ElementRef<HTMLElement>) { }

  ngOnInit(): void {
    this.loggedUser=localStorage.getItem('userId')?.toString();
    this.isLoggedIn=(localStorage.getItem('loggedStatus')?.toString()=="true");

    this.service.getRefresh().subscribe(()=>
    this.getAllTweets());
    this.getAllTweets();
  }
  
  getAllTweets()
  {
    this.service.getTweets().subscribe((x)=> {
      console.log(x);
      this.tweets=x;
    });
  }
  deleteTweet(id:any)
  {
    let userid=localStorage.getItem('userId')?.toString();
    this.service.deleteTweet(userid,id);
  }
  postTweet()
  {
      if(localStorage.getItem('loggedStatus')?.toString()!="true")
      {
        this.router.navigateByUrl('Login');
      }
      else
      {
        let userid=localStorage.getItem('userId')?.toString();
        this.service.postTweet(this.newTweet.value,userid);
      }
  }
  updateTweet()
  {
    let userid=localStorage.getItem('userId')?.toString();
    this.service.updateTweet(this.newTweet.value,userid,this.tweetid);
    this.Tweetmessage='';
    this.isEdit=false;
  }
  replyTweet(id:any)
  {
    let userid=localStorage.getItem('userId')?.toString();
    this.service.replyTweet(this.tweet.value,id,userid);
    this.tweet=new FormGroup({
      Reply_Message:new FormControl(''),
     })
  }
  checkLikesCount(likescount:any):boolean
  {
    if(likescount>0)
     return true;
    else
     return false;
  }

  checkDisLikesCount(dislikescount:any):boolean
  {
    if(dislikescount>0)
     return true;
    else
     return false;
  }

  likeTweet(id:any,liketype:number)
  {
    let userid=localStorage.getItem('userId')?.toString();
    this.service.likeTweet(id,userid,liketype);
  }
  searchByUser()
  {
    if(this.Tweetsearch.controls['searchInput'].value=='')
    {
      this.service.getTweets().subscribe((x)=> {
        console.log(x);
        this.tweets=x;
      });
    }
    else{
      this.service.getTweetByUserId(this.Tweetsearch.controls['searchInput'].value).subscribe((x:any)=>{
        console.log(x);
        if(x!=null && x.length>0)
        {
          this.tweets=x;
        }
        else{
          alert("No tweets are available")
        }
      } 
      );
    }
  }
  
  openEditor(message:any,id:any)
  {
    this.Tweetmessage=message;
    this.tweetid=id;
    this.isEdit=true;
  }
  checkSameUser(username:any):boolean{
    this.loggedUser=localStorage.getItem('userId')?.toString();
    this.isLoggedIn=(localStorage.getItem('loggedStatus')?.toString()=="true");
    if(this.isLoggedIn && (this.loggedUser=='admin'|| this.loggedUser==username))
    {
      return true;
    }
    else
    {
      return false;
    }  
  }

}
