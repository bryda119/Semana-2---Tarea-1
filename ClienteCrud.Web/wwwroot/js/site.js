(function () {
  function initIcons() {
    if (window.lucide && typeof window.lucide.createIcons === "function") {
      window.lucide.createIcons();
    }
  }

  function initNav() {
    var toggle = document.getElementById("navToggle");
    var links = document.getElementById("navLinks");
    if (!toggle || !links) return;

    toggle.addEventListener("click", function () {
      links.classList.toggle("open");
    });
  }

  function initTooltips() {
    if (!window.bootstrap || !bootstrap.Tooltip) return;
    document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (el) {
      bootstrap.Tooltip.getOrCreateInstance(el);
    });
  }

  function initClientSearch() {
    var input = document.getElementById("clientSearch");
    var table = document.getElementById("clientsTable");
    var emptyFilter = document.getElementById("emptyFilterState");
    if (!input || !table) return;

    var rows = Array.prototype.slice.call(table.querySelectorAll("tbody tr[data-search]"));

    function applyFilter() {
      var query = input.value.trim().toLowerCase();
      var visible = 0;

      rows.forEach(function (row) {
        var haystack = (row.getAttribute("data-search") || "").toLowerCase();
        var match = !query || haystack.indexOf(query) !== -1;
        row.hidden = !match;
        if (match) visible += 1;
      });

      if (emptyFilter) {
        var showEmpty = visible === 0 && rows.length > 0;
        emptyFilter.hidden = !showEmpty;
        emptyFilter.classList.toggle("is-hidden", !showEmpty);
      }
    }

    input.addEventListener("input", applyFilter);
    applyFilter();
  }

  function initFormLoading() {
    document.querySelectorAll("form[data-loading]").forEach(function (form) {
      form.addEventListener("submit", function () {
        var button = form.querySelector('[type="submit"]');
        if (!button || button.disabled) return;

        button.disabled = true;
        var label = button.querySelector(".btn-label");
        var icon = button.querySelector(".btn-icon-slot");
        if (label) label.textContent = "Guardando...";
        if (icon) {
          icon.innerHTML = '<span class="btn-spinner" aria-hidden="true"></span>';
        }
      });
    });
  }

  document.addEventListener("DOMContentLoaded", function () {
    initIcons();
    initNav();
    initTooltips();
    initClientSearch();
    initFormLoading();
  });
})();
