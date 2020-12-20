<template>
  <v-card class="mx-auto" tile>
    <v-combobox small-chips solo v-bind:items="categoryList"></v-combobox>
    <v-divider />
    <v-list style="max-height: 800px" class="overflow-y-auto">
      <template v-for="(item, index) in ruleList">
        <RuleItem v-bind:item="item" :key="index" />
      </template>
    </v-list>
  </v-card>
</template>

<script>
import RuleItem from "./RuleItem";
import { getAllRuleCategories } from "../../api/rule";
export default {
  name: "RuleList",
  components: { RuleItem },
  data() {
    return {
      categoryList: [],
      ruleList: [],
    };
  },
  async created() {
    console.log("Retrieving information");
    var response = await getAllRuleCategories();
    this.categoryList = response;
    console.log(this.categoryList);
  },
};
</script>

<style></style>
