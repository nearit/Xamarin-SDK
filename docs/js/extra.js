$(function(){

  $('.code-native').replaceWith(function() {
    return $('<div class="code-switcher"> <button class="tab-native" onclick="switchCode(0)">Native</button> <button class="tab-bridge" onclick="switchCode(1)">Bridge</button> </div><pre class="code native"><code>' + this.innerHTML + '</code></pre>');
  });

  $('.code-bridge').replaceWith(function() {
    return $('<pre class="code bridge"><code>' + this.innerHTML + '</code></pre>');
  });

  switchCode(0);

})

function switchCode(item) {

  // hide all code blocks
  $('.code').hide();

  // show selected code blocks and change selected tab
  switch (item) {
    case 0:
      $('.native').show();
      $('.tab-native').addClass('active')
      $('.tab-bridge').removeClass('active')
      break;
    case 1:
      $('.bridge').show();
      $('.tab-bridge').addClass('active')
      $('.tab-native').removeClass('active')
      break;
    default:
      $('.native').show();
      $('.tab-native').addClass('active')
      $('.tab-bridge').removeClass('active')
      break;
  }

}
