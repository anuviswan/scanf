<template>
  <v-container>
    <v-row>
      <v-col cols="3" align-self="start"
        ><RuleList v-on:onItemSelectionChanged="handleOnItemSelectionChanged"
      /></v-col>
      <v-col cols="9"
        ><v-card><RuleDescription v-bind:item="ruleSelected"/></v-card
      ></v-col>
    </v-row>
  </v-container>
</template>

<script>
import RuleList from "./rules/RuleList";
import RuleDescription from "./rules/RuleDescription";
export default {
  name: "Shell",
  components: {
    RuleList,
    RuleDescription,
  },
  props: {
    msg: String,
  },
  data() {
    return {
      ruleSelected: {},
    };
  },
  methods: {
    handleOnItemSelectionChanged(item) {
      console.log(item);
      this.ruleSelected = item;
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
