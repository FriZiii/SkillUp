// videojs.ts component
import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from 
  '@angular/core';
  import videojs from 'video.js';

  
  @Component({
    selector: 'app-videojs',
    templateUrl: './videojs.component.html',
    standalone: true,
    styleUrls: ['./videojs.component.scss'],
    encapsulation: ViewEncapsulation.None,
  })
  export class VjsPlayerComponent implements AfterViewInit, OnDestroy {
    @ViewChild('target', {static: true}) target!: ElementRef;

@Input() videoLink!: string;

options = {

autoplay: false,

notSupportedMessage: 'Invalid Video URL', // to change the default message

}

player!: any;

qualityLevels: any;

 

constructor() { }

 

ngOnInit(): void {

}

 

ngAfterViewInit(): void {

     this.readyVideojsPlayer()

}

 

readyVideojsPlayer() {

    this.player = videojs(this.target.nativeElement, this.options, function () { });

    this.player.src({

       src: this.videoLink,

      type: 'video/mp4'

   })

}
  
    ngOnDestroy() {
      // destroy player
      if (this.player) {
        this.player.dispose();
      }
    }
  }