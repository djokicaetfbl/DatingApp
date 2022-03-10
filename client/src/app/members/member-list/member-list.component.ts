import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  //members: Member[] | undefined; // (komentar u ngOnInit objasnjava)
  members$: Observable<Member[]> | undefined; // sa ovim $ na kraju naziva atributa naglasavamo da je rijec o Observable-u

  constructor(private memberService: MembersService) {}

  ngOnInit(): void {
    //this.loadMembers();
    this.members$ = this.memberService.getMembers(); // jer smo uveli reaktivnost pomocu operatora 'of' iz rxjs metodi getMembers() iz membersService rekla da upravo pomocu operatora 'of' vraca Observable
  }

  /*loadMembers() { ovo nam vise ne treba jer smo uveli reaktivnost, (komentar u ngOnInit objasnjava)
    this.memberService.getMembers().subscribe((members) => {
      this.members = members;
    });
  }*/
}
