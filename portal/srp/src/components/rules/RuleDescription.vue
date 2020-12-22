<template>
  <v-card outlined tile>
    <VueShowdown
      v-bind:markdown="this.description"
      flavor="vanilla"
      :options="{ emoji: true }"
      class="text-left px-3"
  /></v-card>
</template>

<script>
import { VueShowdown } from "vue-showdown";
import { getRuleDescription } from "../../api/rule";
export default {
  name: "RuleDescription",
  components: { VueShowdown },
  props: {
    item: {},
  },
  data() {
    return {
      description: "## Avoid using async void",
    };
  },
  created() {
    console.log(this.description);
  },
  watch: {
    item: async function(newValue, oldValue) {
      console.log("change caught in watch - " + newValue + "," + oldValue);
      console.log(this.description);
      const response = await getRuleDescription(newValue);
      console.log(atob(response));
      this.description = atob(response);
      console.log(this.description);
    },
  },
};
</script>

<style></style>
