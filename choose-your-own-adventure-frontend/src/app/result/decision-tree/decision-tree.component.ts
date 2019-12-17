import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {QuestionTreeNode} from '../../models/question-tree-node';
import {QuestionAnswer} from '../../models/question-answer.enum';

declare var $: any;

@Component({
  selector: 'app-decision-tree',
  templateUrl: './decision-tree.component.html',
  styleUrls: ['./decision-tree.component.scss']
})
export class DecisionTreeComponent implements OnInit {
  @ViewChild('chartContainer', {static: false}) chartContainer: ElementRef;

  @Input() dataSource: QuestionTreeNode;

  constructor() { }

  ngOnInit() {
    $('#chart-container').orgchart({
      data : this.dataSource,
      nodeTemplate: this.questionNodeTemplate
    });
  }

  private questionNodeTemplate(data: QuestionTreeNode): string {
    let template = '';

    if (data.type != null) {
      template = `
        <div style="width: 100%; min-width: 130px" class="title">
        ${data.type === QuestionAnswer.Positive ? 'Yes' : 'No'}</div>`;
    }

    template += `
        <div >${data.text}</div>
        <input class="questionId" style="display: none" value="${data.id}">`;

    return template;
  }

  highlightNodes(ids: number[]) {
    this.chartContainer.nativeElement.querySelectorAll('.questionId').forEach(elem => {
      const val = Number(elem.getAttribute('value'));
      if ( ids.indexOf(val) !== -1) {
        elem.parentElement.style.backgroundColor = 'rgba(238,217,54,.5)';
      }
    });
  }
}
