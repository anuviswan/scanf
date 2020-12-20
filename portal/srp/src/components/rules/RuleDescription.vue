<template>
  <VueShowdown
    v-bind:markdown="this.description"
    flavor="github"
    :options="{ emoji: true }"
  />
</template>

<script>
import { VueShowdown } from "vue-showdown";
//import { getRuleDescription } from "../../api/rule";
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
  async created() {
    // console.log(this.item);
    // const response = await getRuleDescription(this.item.description);
    // this.description = response;
  },
};
</script>

<style></style>
