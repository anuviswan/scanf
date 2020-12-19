<template>
  <v-card class="mx-auto" tile>
    <v-list style="max-height: 800px" class="overflow-y-auto">
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
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
        {
          title: "Empty methods could be removed",
          category: "Code Smell",
        },
        {
          title: "Asynchronous methods should not return void",
          category: "Bug",
        },
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
