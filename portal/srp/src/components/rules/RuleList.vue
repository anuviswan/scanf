<template>
  <v-card class="mx-auto" tile>
    <v-list>
      <template v-for="(item, index) in ruleList">
        <RuleItem v-bind:item="item" :key="index" />
      </template>
    </v-list>
  </v-card>
</template>

<script>
import RuleItem from "./RuleItem";
export default {
  name: "RuleList",
  components: { RuleItem },
  data() {
    return {
      ruleList: [
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
      ],
    };
  },
};
</script>

<style></style>
