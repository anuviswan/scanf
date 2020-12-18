<template>
  <div class="row">
    <div class="col-3">
      Rule here
    </div>
    <div class="col-9">
      Description here
    </div>
  </div>
</template>

<script>
export default {
  name: "Shell",
  props: {
    msg: String,
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
