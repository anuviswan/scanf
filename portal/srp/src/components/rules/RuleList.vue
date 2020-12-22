<template>
  <v-card outlined tile>
    <v-list style="max-height: 800px" class="overflow-y-auto">
      <v-list-item-group active-class="blue-grey darken-3--text dark">
        <template v-for="(item, index) in ruleList">
          <RuleItem
            v-bind:item="item"
            :key="index"
            v-on:onItemClicked="handleOnItemClicked"
          />
        </template>
      </v-list-item-group>
    </v-list>
  </v-card>
</template>

<script>
import RuleItem from "./RuleItem";
import { getAllRuleCategories, getAllItemsForCategory } from "../../api/rule";
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
    var responseCategories = await getAllRuleCategories();
    this.categoryList = responseCategories;

    var responseItems = await getAllItemsForCategory("Code Smell");
    this.ruleList = responseItems;
    console.log(this.categoryList);
    console.log(this.ruleList);
  },
  methods: {
    handleOnItemClicked(item) {
      this.$emit("onItemSelectionChanged", item);
    },
  },
};
</script>

<style></style>
