
$(function() {

    $('.custom-dropdown').on('show.bs.dropdown', function() {
      // $(this).find('.dropdown-menu').first().stop(false, false).slideDown();
       // $(this).find('.dropdown-menu').addClass('active');
       var that = $(this);
      setTimeout(function(){
          that.find('.dropdown-menu').addClass('active');
      }, 100);
      
  
    });
    $('.custom-dropdown').on('hide.bs.dropdown', function() {
      $(this).find('.dropdown-menu').removeClass('active');
    });
  
  });
  if (!('boxShadow' in document.body.style)) {
    document.body.setAttribute('class', 'noBoxShadow');
}

document.body.addEventListener("click", function(e) {
    var target = e.target;
    if (target.tagName === "INPUT" &&
        target.getAttribute('class').indexOf('liga') === -1) {
        target.select();
    }
});