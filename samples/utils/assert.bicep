provider utils

resource assert 'Assert' = {
  name: 'This should fail!'
  condition: false
}
