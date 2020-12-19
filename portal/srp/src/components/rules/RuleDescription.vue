<template>
  <VueShowdown
    v-bind:markdown="this.description"
    flavor="github"
    :options="{ emoji: true }"
  />
</template>

<script>
import { VueShowdown } from "vue-showdown";

export default {
  name: "RuleDescription",
  components: { VueShowdown },
  data() {
    return {
      description: "## Avoid using async void",
    };
  },
};
</script>

<style></style>
