provider '../../src/http/build/provider.tgz'

resource forecast 'HttpRequest' = {
  uri: 'https://api.weather.gov/gridpoints/SEW/131,69/forecast'
  format: 'raw'
}

output forecast string = forecast.body
