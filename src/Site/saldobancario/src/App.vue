<template>
  <div id="app">
    <div id="nav">
      <router-link to="/">Home</router-link>|
      <router-link to="/about">About</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
  width: 100%;
  margin: auto;
}
table {
  margin: 0px;
  padding: 0px;
  list-style-type: nome;
  width: 100%;
  border-collapse: collapse;
}
table, th, td {
  border: 1px solid black;
}
table thead th {
  background-color: rgb(209, 209, 212) !important;
  font-size: 1em;
  padding: 3px 10px;
  text-align: left !important;
}
table tr td {
  padding: 3px 10px;
  font-size: 1em;
  margin-bottom: 2px;
  color: #3e5252;
  text-align: left;
}
.table-limit {
  min-height: 0.01%;
  height: 400px;
  overflow-y: scroll;
}
hr {
  margin: 20px;
}
button {
  font-size: 1em;
  background-color: rgb(209, 209, 212) !important;
  font-weight: bold;
  cursor: pointer;
  border: 1px solid black;
  cursor: pointer;
  padding: 5px 10px;
  border-radius: 2px;
  border-bottom: 3px solid black;
}
label {
  font-size: 1.5em;
  font-weight: bold;
}
.box-result {
  display: flex;
  justify-content: space-between;
  padding: 0px 20px 0px 20px;
}
</style>
