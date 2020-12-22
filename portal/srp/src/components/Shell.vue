<template>
  <v-container>
    <v-row dense align-content="left">
      <v-col cols="3" class="text-left"
        ><RuleList v-on:onItemSelectionChanged="handleOnItemSelectionChanged"
      /></v-col>
      <v-col cols="9" class="text-left"
        ><RuleDescription v-bind:item="ruleSelected"
      /></v-col>
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
<style scoped></style>
