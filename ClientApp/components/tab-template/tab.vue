<template>
  <div>
      <ul class="nav nav-tabs" role="tablist">
          <li v-for="tab in tabs" :key="tab.name">
              <a class="nav-link" data-toggle="tab" :href="tab.ref" role="tab">{{name}}</a>
          </li>    
      </ul>
    
    <div class="tab-content"><slot></slot></div>
  </div>
</template>

<script>
    import tabContent from './tab_content';

    export default {
        data() {
            return {}
        },
        props: [ 'tabs', 'tab_ref'],
        components: {
            'tab-content': tabContent
        },
        methods: {
            active(data) {
                this.records.tabsContent.forEach((element, index) => {
                    if (element.class === 'tab-pane fade active show' ) {
                        this.records.tabsContent[index].class = 'tab-pane fade';
                    }
                    if (element.name === data.name) {
                        this.records.tabsContent[index].class = 'tab-pane fade active show';
                    }
                });
            },
        }
    }   
</script>
